# CadastroDeCampeonato
API Rest com Cadastro de campeonato a partir das quartas de finais, realiza os jogos automáticamente com critério de penaltis no desempate!

## Como executar o projeto:
- Clonar o Projeto
```
git clone https://github.com/Pa1loh/CadastroDeCampeonato.git
```
- Abrir em IDE com suporte ao [.Net6](<https://dotnet.microsoft.com/en-us/download/dotnet/6.0>) (Recomendo [Visual Studio 2022](<https://visualstudio.microsoft.com/pt-br/vs/>))
- Executar o comando no console nugget o seguinte comando, para gerar o banco de dados
```
Update-Database
````
- Ao rodar a aplicação a documentação irá abrir com o Swagger:

  <https://localhost:7052/swagger>




## Principais End-Points:
- **POST** api/time : Cadastrar times na API
- **POST** api/campeonato : Cadastrar campeonato na API
- **POST** api/campeonato/time-campeonato : Cadastrar um time no campeonato (máximo 8 times)
- **POST** api/campeonato/gerar-campeonato : Gera um campeonato com as chaves, partidas e resultados com **pênaltis**, na API e retorna o time campeão (O campeonato só é gerado 1 vez, e caso tenha 8 times)
- **GET** api/campeonato/historico-campeonato : Retorna um histórico das partidas ocorridas no campeonato escolhido
- **GET** api/campeonato/pontuacao-time-campeonato : Retorna a pontuação baseadas em a cada 1 gol 1 ponto,de um time no campeonato
- **GET** api/campeonato/campeao-campeonato : Retorna o time campeao do campeonato

## Observações
- Critério desempate por pênaltis
Recebe os ids da PartidaTime, entra em um laço de cobrança de penaltis, retorna o id do time vencedor
- Banco de dados
Foi utilizado o Sqlite com os frameworks:
```
 Microsoft.EntityFrameworkCore.Sqlite Version="6.0.10"
 Microsoft.EntityFrameworkCore.SqlServer Version="6.0.10"
 Microsoft.EntityFrameworkCore.Tools
```






