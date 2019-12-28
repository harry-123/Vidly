                    //Temp Code
                    var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
                    var roleManager = new RoleManager<IdentityRole>(roleStore);
                    await roleManager.CreateAsync(new IdentityRole("CanManageMovies"));
                    await UserManager.AddToRolesAsync(user.Id, "CanManageMovies");   