using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Core.Domain;
using Data.Core.Domain.Entities;
using Data.Core.Domain.Entities.Identity;
using Data.Persistence;

namespace WebAPI.Seeders
{
    public class AnnouncementsSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationUser user;

        public AnnouncementsSeeder(ApplicationDbContext context)
        {
            _context = context;
            user = _context.Users.FirstOrDefault(u => u.Id != null);
        }

        public async Task<int> SeedAsync()
        {
            if (_context.Announcements.Any()) return -1;
            await _context.Announcements.AddRangeAsync(GetAnnouncements());
            return await _context.SaveChangesAsync();
        }

        private IEnumerable<Announcement> GetAnnouncements()
        {
            var announcements = new List<Announcement>();
            var title = "Lorem Ipsum ";
            var description =
                @"Lorem ipsum dolor sit amet, eu eum affert inermis maluisset, at eos falli vivendum. At eos libris phaedrum, per ex ignota alienum suscipit. Cum enim probatus no. Ex laudem tritani ocurreret vel. Inermis menandri volutpat ei mei. Ea mazim adipiscing dissentiunt qui.
                Elit conceptam ne has, eos aeterno democritum at.Minim movet ex vis, eos reque malorum ut.At aeterno nominavi deseruisse vis, et nobis aeterno vituperata vim.Eu vis meis nonumy, usu ex sint impetus suscipiantur.
                Per euripidis assueverit quaerendum te, eos no eleifend periculis.Pri an lorem erant paulo, ne antiopam consequuntur cum.An solet delectus mea.In usu eius modus iuvaret, vel zril utamur electram cu.Nec duis accusata comprehensam eu, qui id wisi blandit.
                Patrioque repudiandae eum no.Et movet indoctum qui, eam amet suas brute eu.Vim graece oblique salutatus eu.Errem eirmod scribentur cum cu, nonumes graecis eam ad.Prima brute dignissim te his, ut vis offendit delicata adolescens.Pri in tantas bonorum urbanitas.
                Qui tibique nominavi urbanitas eu, cu eos quis option alienum.Mei petentium splendide sadipscing in, sea aperiam reprimique te, at liber volutpat dissentias vim.Sea et nonumes electram consulatu, mea diam voluptaria in.Ne per aliquid commune aliquando, corpora reprehendunt his id.His at sanctus iracundia quaerendum, has ea mandamus repudiandae disputationi.Vim te eleifend complectitur.
                Ei lobortis definiebas vel.Ius fugit prodesset ut.Mentitum disputando signiferumque cu cum.Sed eu lorem exerci adipiscing, ex duis dicant quo.
                An cum facete accumsan.At iusto affert vulputate his.Id posse scribentur mel, saepe alterum qui ut.Saepe graece has id, eu dicat labores nam.
                Has an meis liber phaedrum.Dolor epicurei pericula in mel, ut quo zril labores.Mei ea utamur epicuri, an quem postea his.Mundi phaedrum prodesset cum et, labores dolorem postulant ea pro, clita facete voluptatum ex eum.Vis antiopam splendide voluptaria ei.
                Mel feugait tacimates ne, nec et sale lorem.Ut mel tota delenit reprehendunt, zril apeirian appellantur ex nec, mea cu mutat aeterno.Sea eius movet molestiae ex, sed ad dicta ignota.An eum ceteros prodesset consectetuer, ad solet graeco impetus vis, ei utroque sadipscing eos.Et dicunt expetenda sit.
                Graeco aliquip assueverit te vix, id mutat homero ponderum mea.Dolore elaboraret eu duo, ei nec praesent imperdiet, mel velit congue suavitate eu.Elitr scripserit nec ad, ea sed noluisse instructior, scripta tamquam dissentiunt in mel.Ad discere denique vix, odio posidonium nec ad.Duo id corpora forensibus disputando.Ea nam affert singulis, ei soluta melius virtute sed.";
            
            for (var i = 0; i < 35; i++)
            {
                var severity = (SeverityEnum)new Random().Next(3);
                announcements.Add(Announcement.Create(title + i.ToString(), description, severity, user.Id));
            }

            return announcements;
        }
    }
}
