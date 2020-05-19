/*
配置後スクリプト テンプレート							
--------------------------------------------------------------------------------------
 このファイルには、ビルド スクリプトに追加される SQL ステートメントが含まれています。		
 SQLCMD 構文を使用して、ファイルを配置後スクリプトにインクルードできます。			
 例:      :r .\myfile.sql								
 SQLCMD 構文を使用して、配置後スクリプト内の変数を参照できます。		
 例:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
If (NOT EXISTS(Select * From Company))
Begin
    Insert Into [Company]
        Select 1, 'HOGE'
        Union All
        Select 2, 'Fuga'
        Union All
        Select 3, 'Hege'
End;

If (NOT EXISTS(Select * From Department))
Begin
    Insert Into [Department]
        Select 1, 1, 'HOGE部署'
        Union All
        Select 1, 2, 'FUGA部署'
        Union All
        Select 1, 3, 'HEGE部署'
        Union All
        Select 2, 1, 'PIYO部署'
        Union All
        Select 2, 2, 'POYO部署'
        Union All
        Select 3, 1, 'DIO部署'
End;

If (NOT EXISTS(Select * From Employee))
Begin
    Insert Into [Employee]
        Select 1, 1, 1, 'Aさん'
        Union All
        Select 1, 1, 2, 'Bさん'
        Union All
        Select 1, 1, 3, 'Cさん'
        Union All
        Select 1, 2, 4, 'Dさん'
        Union All
        Select 1, 2, 5, 'Eさん'
        Union All
        Select 1, 3, 6 ,'Fさん'
        Union All
        Select 2, 1, 7, 'Gさん'
        Union All
        Select 2, 1, 8, 'Hさん'
        Union All
        Select 2, 2, 9, 'Iさん'
        Union All
        Select 3, 1, 10, 'Jさん'
End;