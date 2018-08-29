using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cbn.DDDSample.Application.Interfaces.Services;
using Cbn.Infrastructure.Common.Data.Migration.Interfaces;
using Cbn.Infrastructure.Common.DependencyInjection.Interfaces;
using Cbn.Infrastructure.Common.Foundation.Exceptions;
using Cbn.Infrastructure.Common.IO.Interfaces;

namespace Cbn.DDDSample.Application.Services
{
    public class MigrationService : IMigrationService
    {
        private IPathResolver pathResolver;
        private IScopeProvider scopeProvider;

        public MigrationService(
            IPathResolver pathResolver,
            IScopeProvider scopeProvider)
        {
            this.pathResolver = pathResolver;
            this.scopeProvider = scopeProvider;
        }

        public async Task<int> ExecuteAsync()
        {
            var dir = this.pathResolver.ResolveDirectoryPath("../.migration");
            var files = Directory.GetFiles(dir, "*.sql", SearchOption.AllDirectories)
                .OrderBy(x => x)
                .GroupBy(x => Path.GetFileNameWithoutExtension(x))
                .ToArray();
            var duplicate = files.Where(x => x.Count() > 1).SelectMany(x => x).ToArray();
            if (duplicate.Length > 0)
            {
                throw new BizLogicException($"ファイル名に重複があります。{Environment.NewLine}{string.Join(Environment.NewLine,duplicate)}");
            }
            using(var scope = this.scopeProvider.BeginLifetimeScope())
            {
                var migrationHelper = scope.Resolve<IMigrationHelper>();
                await migrationHelper.InitializeDatabaseAsync();
                await migrationHelper.InitializeAsync();
            }
            foreach (var file in files.Select(x => new { Id = x.Key, Path = x.First() }))
            {
                using(var scope = this.scopeProvider.BeginLifetimeScope())
                {
                    await scope.Resolve<IMigrationHelper>().MigrationAsync(file.Id, file.Path);
                }
            }
            return 0;
        }
    }
}