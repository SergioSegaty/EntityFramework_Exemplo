namespace Exemplo01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusSeedDataNaDB : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Status (Nome) VALUES ('Para Fazer');");
            Sql("INSERT INTO Status (Nome) VALUES ('Fazendo');");
            Sql("INSERT INTO Status (Nome) VALUES ('Feito');");
        }
        
        public override void Down()
        {
            Sql("EXTRUNCATE Status;");
        }
    }
}
