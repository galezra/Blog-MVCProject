namespace Shauli_Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ver1_InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        Title = c.String(),
                        Author = c.String(),
                        Url = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Post", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Author = c.String(),
                        PublishDate = c.DateTime(nullable: false),
                        Content = c.String(),
                        Url = c.String(),
                        Image = c.String(),
                        Video = c.String(),
                    })
                .PrimaryKey(t => t.PostId);
            
            CreateTable(
                "dbo.Fan",
                c => new
                    {
                        FanId = c.Int(nullable: false, identity: true),
                        FName = c.String(),
                        LName = c.String(),
                        Genre = c.Int(nullable: false),
                        BDate = c.DateTime(nullable: false),
                        Seniority = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FanId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "PostId", "dbo.Post");
            DropIndex("dbo.Comment", new[] { "PostId" });
            DropTable("dbo.Fan");
            DropTable("dbo.Post");
            DropTable("dbo.Comment");
        }
    }
}
