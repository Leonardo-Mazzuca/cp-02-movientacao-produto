RM 99430 Leonardo de Souza Mazzuca
RM 551234 Luis Miguel Lima Rodrigues

## Projeto C# - API de Movimentação e Produtos

## Descrição

Este projeto é uma API desenvolvida em C# para gerenciar produtos e movimentações de estoque. Permite cadastrar produtos, registrar entradas e saídas de estoque e consultar informações detalhadas sobre movimentações.

## Regras de Negócio Implementadas

- Cadastro de Produtos

- Cada produto deve ter: Codigo_Sku, Nome, Preço e Quantidade em Estoque.

- Produtos não podem ter nome vazio ou quantidade negativa.

- Movimentação de Estoque

Movimentações podem ser de entrada ou saída.

- Para saídas, a quantidade não pode exceder o estoque disponível.

- Toda movimentação deve estar vinculada a um produto existente.

- Consulta de Movimentações

É possível listar todas as movimentações ou filtrar por produto.

## Diagrama de entidades

```yaml
Produto
---------
Id : int
Nome : string
Preco : decimal
Quantidade : int

Movimentacao
-------------
Id : int
ProdutoId : int
Tipo : string (Entrada / Saída)
Quantidade : int
Data : DateTime
```

## Exemplos de Requisições API

1. Listar Produtos

```bash
GET /api/produtos
```

2. Cadastrar Produto

```bash
POST /api/produtos
Body:
{
  "nome": "Chocolate",
  "preco": 12.50,
  "quantidade": 100
}
```

3. Registrar Movimentação

```bash
POST /api/movimentacoes
Body:
{
  "produtoId": 1,
  "tipo": "Entrada",
  "quantidade": 50
}
```

4. Consultar Movimentações de um Produto
```bash
GET /api/movimentacoes/produto/1
```

## Como Executar o Projeto

Clone o repositório:

```bash
git clone https://github.com/seuusuario/seuprojeto.git
```

Navegue até a pasta do projeto:
```bash
cd seuprojeto
```

Restaurar dependências:

```bash
dotnet restore
```

Executar o projeto:

```bash
dotnet run
```

Acesse a API no navegador ou via Postman:

```bash
http://localhost:5000/api
```
