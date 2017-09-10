using DrMturhan.Server.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DrMturhan.Server
{
    public class SeedDbData
    {
        readonly UygulamaDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly UserManager<Kullanici> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public SeedDbData(IWebHost host, UygulamaDbContext context)
        {
            var services = (IServiceScopeFactory)host.Services.GetService(typeof(IServiceScopeFactory));
            var serviceScope = services.CreateScope();
            _hostingEnv = serviceScope.ServiceProvider.GetService<IHostingEnvironment>();
            _roleManager = serviceScope.ServiceProvider.GetService<RoleManager<ApplicationRole>>();
            _userManager = serviceScope.ServiceProvider.GetService<UserManager<Kullanici>>();
            _context = context;
            CreateRoles(); // Add roles
            CreateUsers(); // Add users
            AddLanguagesAndContent();
        }

        private void CreateRoles()
        {
            var rolesToAdd = new List<ApplicationRole>(){
                new ApplicationRole { Name= "Yönetici", Description = "Yönetici"},
                new ApplicationRole { Name= "Hoca", Description = "Öðretim Üyesi Doktor"},
                new ApplicationRole { Name= "Asistan", Description = "Araþtýrma Görevlisi Doktor"},
                new ApplicationRole { Name= "AbdSekreteri", Description = "Anabilim Dalý Sekreteri" },
                new ApplicationRole { Name= "Sekreter", Description = "Sekreter" },
                new ApplicationRole { Name= "Öðrenci", Description = "Öðrenci" },
                new ApplicationRole { Name= "Hasta", Description = "Hasta" }

            };
            foreach (var role in rolesToAdd)
            {
                if (!_roleManager.RoleExistsAsync(role.Name).Result)
                {
                    _roleManager.CreateAsync(role).Result.ToString();
                }
            }
        }
        private void CreateUsers()
        {
            if (!_context.ApplicationUsers.Any())
            {
                _userManager.CreateAsync(new Kullanici { UserName = "drmturhan@hotmail.com", /*FirstName = "Murat", LastName = "Turhan",*/ Email = "drmturhan@hotmail.com", EmailConfirmed = true, CreatedDate = DateTime.Now, IsEnabled = true }, "akd346300").Result.ToString();
                _userManager.AddToRoleAsync(_userManager.FindByNameAsync("drmturhan@hotmail.com").GetAwaiter().GetResult(), "Yönetici").Result.ToString();
                _userManager.AddToRoleAsync(_userManager.FindByNameAsync("drmturhan@hotmail.com").GetAwaiter().GetResult(), "Hoca").Result.ToString();
                _userManager.AddToRoleAsync(_userManager.FindByNameAsync("drmturhan@hotmail.com").GetAwaiter().GetResult(), "Asistan").Result.ToString();
                _userManager.AddToRoleAsync(_userManager.FindByNameAsync("drmturhan@hotmail.com").GetAwaiter().GetResult(), "AbdSekreteri").Result.ToString();
                _userManager.AddToRoleAsync(_userManager.FindByNameAsync("drmturhan@hotmail.com").GetAwaiter().GetResult(), "Sekreter").Result.ToString();
                _userManager.AddToRoleAsync(_userManager.FindByNameAsync("drmturhan@hotmail.com").GetAwaiter().GetResult(), "Öðrenci").Result.ToString();
                _userManager.AddToRoleAsync(_userManager.FindByNameAsync("drmturhan@hotmail.com").GetAwaiter().GetResult(), "Hasta").Result.ToString();
            }
        }
        private void AddLanguagesAndContent()
        {
            if (!_context.Languages.Any())
            {
                _context.Languages.Add(new Language { Locale = "tr", Description = "Türkçe" });
                _context.SaveChanges();
                _context.Languages.Add(new Language { Locale = "en", Description = "English" });
                _context.SaveChanges();
            }

            if (!_context.Content.Any())
            {
                _context.Content.Add(new Content { Key = "TITLE" });
                _context.SaveChanges();
                _context.Content.Add(new Content { Key = "APP_NAV_HOME" });
                _context.SaveChanges();
                _context.Content.Add(new Content { Key = "APP_NAV_LOGIN" });
                _context.SaveChanges();
                _context.Content.Add(new Content { Key = "APP_NAV_LOGOUT" });
                _context.SaveChanges();
                _context.Content.Add(new Content { Key = "APP_NAV_REGISTER" });
                _context.SaveChanges();
                _context.Content.Add(new Content { Key = "APP_NAV_ADMIN" });
                _context.SaveChanges();

            }

            if (!_context.ContentText.Any())
            {
                _context.ContentText.Add(new ContentText { Text = "Murat Turhan", LanguageId = 1, ContentId = 1 });
                _context.ContentText.Add(new ContentText { Text = "Murat Turhan", LanguageId = 2, ContentId = 1 });

                _context.ContentText.Add(new ContentText { Text = "Ana Sayfa", LanguageId = 1, ContentId = 2 });
                _context.ContentText.Add(new ContentText { Text = "Home", LanguageId = 2, ContentId = 2 });

                _context.ContentText.Add(new ContentText { Text = "Giriþ", LanguageId = 1, ContentId = 3 });
                _context.ContentText.Add(new ContentText { Text = "Login", LanguageId = 2, ContentId = 3 });

                _context.ContentText.Add(new ContentText { Text = "Çýkýþ", LanguageId = 1, ContentId = 4 });
                _context.ContentText.Add(new ContentText { Text = "Log out", LanguageId = 2, ContentId = 4 });

                _context.ContentText.Add(new ContentText { Text = "Üye ol", LanguageId = 1, ContentId = 5 });
                _context.ContentText.Add(new ContentText { Text = "Register", LanguageId = 2, ContentId = 5 });

                _context.ContentText.Add(new ContentText { Text = "Yönetici", LanguageId = 1, ContentId = 6 });
                _context.ContentText.Add(new ContentText { Text = "Admin", LanguageId = 2, ContentId = 6 });

                _context.SaveChanges();
            }
        }

    }
}
