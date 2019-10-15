namespace MVCApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenre : DbMigration
    {
        public override void Up()
        {
            Sql("Insert Genres(Name)values('Family')");
            Sql("Insert Genres(Name)values('Comedy')");
            Sql("Insert Genres(Name)values('Love')");
            Sql("Insert Genres(Name)values('Action')");

        }
        
        public override void Down()
        {
        }
    }
}
