using System.Collections.Generic;
using System.Threading.Tasks;

namespace XylyicXenonXamarin.services
{
    public interface IFileStorageService
    {
        Task<IList<string>> GetAdjectives();
        Task<IList<string>> GetNouns();

    }
}