# MilkRun Cliente - Desafio Técnico

Aplicação Windows Forms em VB.NET que consome um endpoint HTTP para consultar dados de viagem (MilkRun) a partir de um número de telefone informado pelo usuário.

## 📌 Objetivo

- Receber o número de telefone (com DDD)
- Enviar o telefone via header `fone` em uma requisição HTTP POST
- Consumir o endpoint informado no desafio
- Exibir os dados retornados na tela de forma amigável

## 🛠 Tecnologias

- VB.NET
- Windows Forms
- HttpClient
- System.Text.Json
- .NET

## ⚙ Funcionalidades

- Validação do telefone (10 ou 11 dígitos)
- Requisição HTTP POST com header personalizado
- Tratamento de erro HTTP
- Leitura e interpretação do JSON
- Exibição resumida das informações:
  - Número da viagem
  - Data
  - Placas
  - Status
  - Lista de paradas
  - 
Se não houver viagem para o telefone informado, o sistema exibe:
Nenhuma viagem encontrada para este telefone.

## 👩‍💻 Desenvolvido por
Laura Cattabriga
