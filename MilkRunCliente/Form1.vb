Imports System.Text.Json  ' Biblioteca usada para ler (parsear) JSON
Public Class Form1

    ' Instância do serviço responsável por chamar a API
    Private ReadOnly service As New MilkRunService()

    ' Evento que executa quando o botão "Consultar" é clicado
    Private Async Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click

        ' 1) Ler e validar telefone
        Dim telefoneDigitado As String = txtTelefone.Text

        If String.IsNullOrWhiteSpace(telefoneDigitado) Then
            MessageBox.Show("Digite um telefone.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtResposta.Text = ""
            Return
        End If

        ' Remove formatação e bloqueia letras
        Dim telefoneSoNumeros As String = ""

        For Each c As Char In telefoneDigitado
            If Char.IsLetter(c) Then
                MessageBox.Show("Não use letras no telefone.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtResposta.Text = ""
                Return
            End If

            If Char.IsDigit(c) Then
                telefoneSoNumeros &= c
            End If
        Next

        ' Valida se tem 10 ou 11 dígitos (DDD + número)
        If telefoneSoNumeros.Length <> 10 AndAlso telefoneSoNumeros.Length <> 11 Then
            MessageBox.Show("Telefone inválido. Use DDD + número (10 ou 11 dígitos).", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtResposta.Text = ""
            Return
        End If

        ' 2) Chamar API
        btnConsultar.Enabled = False
        txtResposta.Text = "Consultando..."

        Try
            ' Chama a API (service faz o POST e devolve o JSON)
            Dim resposta As String = Await service.ConsultarAsync(telefoneSoNumeros)

            ' Parse do JSON
            Using doc As JsonDocument = JsonDocument.Parse(resposta)

                Dim root As JsonElement

                ' Objeto principal do retorno
                If Not doc.RootElement.TryGetProperty("SincronizarResult", root) Then
                    txtResposta.Text = "Resposta inesperada da API."
                    Return
                End If

                ' 3) Verificar se há viagem (NumeroViagem pode vir null ou vazio)
                Dim numeroViagem As String = ""
                Dim tmp As JsonElement

                If root.TryGetProperty("NumeroViagem", tmp) AndAlso tmp.ValueKind = JsonValueKind.String Then
                    numeroViagem = tmp.GetString()
                End If

                If String.IsNullOrWhiteSpace(numeroViagem) Then
                    txtResposta.Text = "Nenhuma viagem encontrada para este telefone."
                    Return
                End If

                ' 4) Extrair campos principais (com TryGetProperty para não quebrar se vier null)
                Dim dataViagem As String = GetStringSafe(root, "DataViagem")
                Dim placaCavalo As String = GetStringSafe(root, "PlacaCavalo")
                Dim placaCarreta1 As String = GetStringSafe(root, "PlacaCarreta1")
                Dim executouOK As String = GetBoolSafe(root, "ExecutouOK")

                ' 5) Montar resumo amigável
                Dim resumo As String = ""
                resumo &= "Número da Viagem: " & numeroViagem & Environment.NewLine
                resumo &= "Data da Viagem: " & dataViagem & Environment.NewLine
                resumo &= "Placa Cavalo: " & placaCavalo & Environment.NewLine
                resumo &= "Placa Carreta: " & placaCarreta1 & Environment.NewLine
                resumo &= "Executou OK: " & executouOK & Environment.NewLine
                resumo &= Environment.NewLine
                resumo &= "PARADAS:" & Environment.NewLine

                ' 6) Destinatarios (paradas)
                Dim destinatarios As JsonElement

                If root.TryGetProperty("Destinatarios", destinatarios) AndAlso destinatarios.ValueKind = JsonValueKind.Array Then

                    If destinatarios.GetArrayLength() = 0 Then
                        txtResposta.Text = "Nenhuma viagem encontrada para este telefone."
                        Return
                    End If

                    For Each parada As JsonElement In destinatarios.EnumerateArray()

                        Dim ordem As String = GetStringSafe(parada, "ordemParada")
                        Dim descricao As String = GetStringSafe(parada, "descricaoParada")
                        Dim cnpj As String = GetStringSafe(parada, "cnpj")
                        Dim endereco As String = GetStringSafe(parada, "endereco")
                        Dim razaoSocial As String = GetStringSafe(parada, "razaoSocial")

                        resumo &= "• " & ordem & " - " & descricao & Environment.NewLine

                        If razaoSocial <> "N/A" Then
                            resumo &= "   Razão Social: " & razaoSocial & Environment.NewLine
                        End If

                        If cnpj <> "N/A" Then
                            resumo &= "   CNPJ: " & cnpj & Environment.NewLine
                        End If

                        If endereco <> "N/A" Then
                            resumo &= "   Endereço: " & endereco & Environment.NewLine
                        End If

                        resumo &= Environment.NewLine
                    Next

                Else
                    txtResposta.Text = "Nenhuma viagem encontrada para este telefone."
                    Return
                End If

                txtResposta.Text = resumo
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Erro ao consultar", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtResposta.Text = ""
        Finally
            btnConsultar.Enabled = True
        End Try

    End Sub

    ' Lê string do JSON com segurança (se não existir / null / vazio -> "N/A")
    Private Function GetStringSafe(obj As JsonElement, propName As String) As String
        Dim v As JsonElement
        If obj.TryGetProperty(propName, v) Then
            If v.ValueKind = JsonValueKind.String Then
                Dim s = v.GetString()
                If String.IsNullOrWhiteSpace(s) Then Return "N/A"
                Return s
            End If
            If v.ValueKind = JsonValueKind.Number Then Return v.ToString()
            If v.ValueKind = JsonValueKind.Null Then Return "N/A"
            Return v.ToString()
        End If
        Return "N/A"
    End Function

    ' Lê boolean do JSON com segurança
    Private Function GetBoolSafe(obj As JsonElement, propName As String) As String
        Dim v As JsonElement
        If obj.TryGetProperty(propName, v) Then
            If v.ValueKind = JsonValueKind.True OrElse v.ValueKind = JsonValueKind.False Then
                Return v.GetBoolean().ToString()
            End If
            If v.ValueKind = JsonValueKind.String Then
                Return v.GetString()
            End If
            Return v.ToString()
        End If
        Return "N/A"
    End Function

End Class