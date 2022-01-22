O programa em questão faz consultas a um banco de dados SQL Server quantas vezes o usuário desejar.
Essas consultas incluem:
- Cadastro de novos usuários na base
- Teste de login em usuários já existemtes

No Main do projeto há um while para validar se o usuário deseja continuar executando comandos no banco, e um Switch-case para poder ser escolhido entre login ou cadastro.

O cadastro é mais simplificado, apenas pegando os dados necessários e enviando para o banco através de um método sem retorno (Void)  nomeado como "cadastrar".

Já o login possui dois métodos, primeiramente ele pega os parâmetros de login e senha passados pelo usuário e envia para um método de validação que retorna um valor em boolean, sendo true caso os dados sejam encontrados no banco ou false caso não sejam.
No caso de retorno true é lançado um método Void para executar o login com os dados passados.
