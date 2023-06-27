$BD_SERVER_NAME = "CT-C-0013L\SQLEXPRESS"
$BD_DATABASE_NAME = "RedBosch"
$strconn = "Data Source=" + $BD_SERVER_NAME + ";Initial Catalog=" + $BD_DATABASE_NAME + ";Integrated Security=True;TrustServerCertificate=true"
dotnet ef dbcontext scaffold $strconn Microsoft.EntityFrameworkCore.SqlServer --force -o Model