using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace XylyicXenonXamarin.services
{
    public class FileStorageService : IFileStorageService
    {
        private IList<string> _adjectives = new List<string>();
        private IList<string> _nouns = new List<string>();

        private string _adjectiveFileName = "adjectives.list";
        private string _nounFileName = "nouns.list";


        private async Task<IList<string>> GetList(string filename)
        {
            var output = new List<string>();

            using (var stream = await FileSystem.OpenAppPackageFileAsync(filename))
            {
                using (var reader = new StreamReader(stream))
                {
                    while (true)
                    {
                        var nextLine = await reader.ReadLineAsync();
                        if (string.IsNullOrWhiteSpace(nextLine))
                            break;

                        output.Add(nextLine);
                    }
                }
            }
            return output;
        }

        public async Task<IList<string>> GetAdjectives()
        {
            if (_adjectives.Any())
                return _adjectives;

            _adjectives = await GetList(_adjectiveFileName);
            return _adjectives;
        }

        public async Task<IList<string>> GetNouns()
        {
            if (_nouns.Any())
                return _nouns;

            _nouns = await GetList(_nounFileName);
            return _nouns;
        }
    }
}
