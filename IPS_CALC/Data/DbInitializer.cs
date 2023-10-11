using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using IPS.DAL;
using IPS.DAL.Context;
using IPS_CALC.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
using ips = IPS.DAL;
namespace IPS_CALC.Data
{
    internal class DbInitializer
    {
        private DBContext _db;
        private ILogger<DbInitializer> _Logger;

        public DbInitializer
            (DBContext db, ILogger<DbInitializer> Logger)
        {
            _db = db; _Logger = Logger;
        }
        public void Initialized()
        {
            _db.Database.EnsureDeleted();
            _db.Database.Migrate();
        }
        public async Task InitializeAsync()
        {
            //await _db.Database.EnsureDeletedAsync()
            //   .ConfigureAwait(false);

            await _db.Database.MigrateAsync();

            if (await _db.iPs.AnyAsync())  return;
            //AddIps();

            await InitializeIPsAsync();

            await InitializeCargoAsynk();

            if (await _db.iPs.AnyAsync()) 
                 await SettingUpLinksAsync();

            var sdffgdijgd = _db.iPs.Include(c => c.IPS2Cargoes);
        }


        private const int __LinksCount = 10;
        private IPS2Cargo[] _IPS2Cargoes;
        /// <summary>
        /// Установка связей
        /// с грузами и ипс
        /// </summary>
        private void SettingUpLinks()
        {
            var ipss = _db.iPs.Include(c => c.IPS2Cargoes).Take(10).ToArray();
            var cargs = _db.Cargoes.Include(c => c.IPS2Cargoes).Take(10).ToArray();
            _IPS2Cargoes = new IPS2Cargo[__LinksCount];
            for (int i = 0; i < ipss.Length; i++)
            {
                for (int j = 0; j < __LinksCount; j++)
                {
                    _IPS2Cargoes[j] = new IPS2Cargo()
                    {
                        IPS = ipss[i],
                        Cargo = cargs[j],
                    };
                    ipss[i].IPS2Cargoes.Add(_IPS2Cargoes[j]);
                }
            }
            _db.UpdateRange(ipss);
            _db.SaveChanges();
        }
        /// <summary>
        /// Установка связей
        /// с грузами и ипс
        /// </summary>
        private async Task SettingUpLinksAsync()
        {
            var ipss = _db.iPs.Include(c => c.IPS2Cargoes).Take(10).ToArray();
            var cargs = _db.Cargoes.Include(c => c.IPS2Cargoes).Take(10).ToArray();
            _IPS2Cargoes = new IPS2Cargo[__LinksCount];
            for (int i = 0; i < ipss.Length; i++)
            {
                for (int j = 0; j < __LinksCount; j++)
                {
                    _IPS2Cargoes[j] = new IPS2Cargo()
                    {
                        IPS = ipss[i],
                        Cargo = cargs[j],
                    };
                    ipss[i].IPS2Cargoes.Add(_IPS2Cargoes[j]);
                }
            }
            _db.UpdateRange(ipss);
            await _db.SaveChangesAsync();
        }

        private const int __CargoCount = 10;
        private Cargo[] _Cargos;
        /// <summary>
        /// Инициализировании 10
        /// грузов и добавление в БД
        /// </summary>
        /// <returns></returns>
        private async Task InitializeCargoAsynk()
        {
            var rnd = new Random();
            _Cargos = new Cargo[__CargoCount];
            _Cargos = Enumerable.Range(1, __CargoCount).
                Select(i => new Cargo
                {
                    Name = $"Груз {i}",
                    Weight =rnd.Next(1000)
                }).ToArray();

            await _db.Cargoes.AddRangeAsync(_Cargos);
            await _db.SaveChangesAsync();

        }
        private const int __IpsCount = 10;
        private ips.IPS[] _IPs;
        /// <summary>
        /// Инициализирование 10 ипс
        /// и добавление в БД
        /// </summary>
        /// <returns></returns>
        private async Task InitializeIPsAsync()
        {
            _IPs = new ips.IPS[__IpsCount];
            for (int i = 0; i < __IpsCount; i++)
                _IPs[i] = new ips.IPS
                {
                    Name = $"ИПС: {i + 1}",
                    Square = (decimal)0.5,
                };

            await _db.iPs.AddRangeAsync(_IPs);
            await _db.SaveChangesAsync();
        }

        #region Помойка
        /// <summary>
        /// Тестовый метот создания
        /// ипс и груза, а
        /// так же связи между ними
        /// </summary>
        private void AddIps()
        {

            var cargo1 = new Cargo()
            {
                Name = "Тарелка 9",
                Weight = (decimal)60.233
            };

            var cargo2 = new Cargo()
            {
                Name = "Пятачек",
                Weight = (decimal)5000.233
            };

            var ips = new ips.IPS()
            {
                Name = "МП-600",
                Square = (decimal)0.05
            };
            var ips2 = new ips.IPS()
            {
                Name = "МП-6",
                Square = (decimal)1
            };

            //Первая запись в промежуточную таблицу
            var b2lib1 = new IPS2Cargo();
            b2lib1.Cargo = cargo1;
            b2lib1.IPS = ips;

            //Вторая запись в промежуточную таблицу
            var b2lib2 = new IPS2Cargo();
            b2lib2.Cargo = cargo2;
            b2lib2.IPS = ips;

            //третья запись
            var b3lib3 = new IPS2Cargo();
            b3lib3.Cargo = cargo2;
            b3lib3.IPS = ips2;

            //Связывние данных в БД
            ips.IPS2Cargoes.Add(b2lib1);
            ips.IPS2Cargoes.Add(b2lib2);
            ips2.IPS2Cargoes.Add(b3lib3);

            _db.iPs.Add(ips);
            _db.iPs.Add(ips2);
            _db.Cargoes.Add(cargo1);
            _db.Cargoes.Add(cargo2);
            _db.SaveChanges();

            //создаю еще одну запись 
            var b3lib4 = new IPS2Cargo();
            b3lib4.Cargo = cargo1;
            b3lib4.IPS = ips2;


            var ipss = _db.iPs.Include(c => c.IPS2Cargoes).FirstOrDefault(x => x.Id == 2);
            ipss.IPS2Cargoes.Add(b3lib4);
            _db.Update(ipss);
            _db.SaveChanges();
            var sdffgdijgd = _db.iPs.Include(c => c.IPS2Cargoes);


            //var cargs = _db.Cargoes.Include(c=>c.IPS2Cargoes);

            //var namesIPS = ipss.Select(c => c.Name).ToList();
            //var cargNames = ipss.Select(c => c.IPS2Cargoes).FirstOrDefault().Select(g=>g.Cargo).Select(g=>g.Name).ToArray();
            //var Weights = ipss.Select(c => c.IPS2Cargoes).FirstOrDefault().Select(g => g.Cargo).Select(g => g.Weight).ToArray();
        }
        #endregion
    }
}
