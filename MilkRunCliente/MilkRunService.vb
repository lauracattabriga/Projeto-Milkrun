Imports System.Net.Http

Public Class MilkRunService

    ' URL fixa do endpoint informado no desafio
    Private Const Endpoint As String =
        "https://qa3orionbr-preprod.cevalogistics.com/WCFOrionMobilityMilkRun/Servicos/SincronizarService.svc/Sincronizar"

    ' Método assíncrono que chama a API e devolve o JSON como texto
    Public Async Function ConsultarAsync(telefone As String) As Task(Of String)

        Using http As New HttpClient()

            ' Evita ficar esperando para sempre se a API travar
            http.Timeout = TimeSpan.FromSeconds(30)

            ' Header exigido pelo desafio
            http.DefaultRequestHeaders.Remove("fone") ' garante que não duplica
            http.DefaultRequestHeaders.Add("fone", telefone)

            ' Cookie
            ' http.DefaultRequestHeaders.Remove("Cookie")
            ' http.DefaultRequestHeaders.Add("Cookie", CookieHeader)

            ' POST com corpo vazio (a API recebe o telefone pelo header)
            Dim content As New StringContent("")

            ' Envia a requisição e espera a resposta sem travar a interface (Await)
            Dim resp As HttpResponseMessage = Await http.PostAsync(Endpoint, content)

            ' Lê todo o corpo da resposta como texto (JSON)
            Dim body As String = Await resp.Content.ReadAsStringAsync()

            ' Se o HTTP não for sucesso (ex: 401/403/500), gera um erro para o Form1 tratar no Catch
            If Not resp.IsSuccessStatusCode Then
                Throw New Exception($"Erro HTTP {(CInt(resp.StatusCode))} - {resp.ReasonPhrase}{Environment.NewLine}{body}")
            End If

            ' Se deu sucesso (200), devolve o JSON para o Form1
            Return body

        End Using
    End Function
End Class