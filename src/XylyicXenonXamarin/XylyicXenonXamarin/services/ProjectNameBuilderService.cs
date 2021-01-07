using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XylyicXenonXamarin.services
{
    public class ProjectNameBuilderService : IProjectNameBuilderService
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly Random _rng;

        public ProjectNameBuilderService(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
            _rng = new Random();
        }

        public async Task<string> GetProjectName()
        {
            var builder = new StringBuilder("Project ");

            var adjectives = await _fileStorageService.GetAdjectives();
            var adjective = adjectives[_rng.Next(0, adjectives.Count - 1)];

            builder.Append(adjective + " ");

            var nouns = await _fileStorageService.GetNouns();
            var noun = nouns[_rng.Next(0, nouns.Count - 1)];

            builder.Append(noun);

            return builder.ToString();
        }
    }
}
