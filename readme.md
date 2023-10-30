# Burger Royale

Esta solução foi desenvolvida como uma API a ser utilizada na gestão de produtos e pedidos de uma lanchonete.


# Execução do Docker Compose para o projeto BurgerRoyale.API

Este guia demonstrará como executar o projeto BurgerRoyale.API usando Docker Compose.

## Pré-requisitos

Antes de prosseguir, certifique-se de que você tenha o Docker e o Docker Compose instalados em sua máquina.

- [Docker](https://docs.docker.com/get-docker/)
- [Docker Compose](https://docs.docker.com/compose/install/)

## Instruções

1. Clone o repositório do projeto BurgerRoyale.API, caso ainda não tenha feito isso:

   ```shell
   git clone https://github.com/BrunoICardoso/TechChallenge.git
   ```

2. Navegue para o diretório raiz do projeto:

   ```shell
   cd <NOME_DO_DIRETORIO_DO_PROJETO>
   ```
   
3. Agora, você está pronto para iniciar o Docker Compose. Execute o seguinte comando no terminal:

   ```shell
   docker-compose build
   ```
   
    Obs.: Esse comando é necessário apenas no caso de alguma alteração ter sido realizada no código da aplicação. Assim garantimos que a execução será realizada com a versão mais recente de nosso código.

4. Em seguida, execute o seguinte comando no terminal:

   ```shell
   docker-compose up
   ```
   
    Isso iniciará os serviços definidos no arquivo `docker-compose.yml`, que incluem a aplicação `burgerroyale.api` e um banco de dados `db`.

5. Após a conclusão do processo de inicialização, você poderá acessar a API BurgerRoyale em http://localhost:5000. Certifique-se de que a aplicação esteja em execução e acessível.

    -  No endereço http://localhost:5000/swagger é possível acessar a documentação dos endpoints disponíveis na API.
    - Uma collection do Postman (`BurgerRoyale.postman_collection.json`) também se encontra disponível no repositório para facilitar a utilização da API.

6. Para encerrar os serviços e remover os contêineres, pressione Ctrl + C no terminal e execute o seguinte comando
   ```shell
   docker-compose down
   ```
