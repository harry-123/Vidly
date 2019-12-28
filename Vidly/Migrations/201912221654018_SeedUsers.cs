namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'add29deb-9b87-45eb-abd6-6480114b1c38', N'guest@vidly.com', 0, N'ABwioFMAdrwyahO3duqMxbmTFK31cRpmbASvrQkx7AYOBv5TCGAHgvx81I0VAESQAA==', N'7c8bd65b-46d2-4372-8a0d-fb053ef84e3d', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'e67a1b9f-5e53-4224-bdbf-c44ea6fcea1c', N'admin@vidly.com', 0, N'ABkVcjPFGeD79z7VbaoHcK5LWhvP/l1UlSzCr1KlwPzW/kWmLOAXBogcKTqL+f9xbw==', N'3a8518da-501f-4955-8e1c-838cf53cf9a8', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'9f339b51-57c1-451e-be07-eef73c9d9433', N'CanManageMovies')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e67a1b9f-5e53-4224-bdbf-c44ea6fcea1c', N'9f339b51-57c1-451e-be07-eef73c9d9433')
            ");
        }
        
        public override void Down()
        {
        }
    }
}
