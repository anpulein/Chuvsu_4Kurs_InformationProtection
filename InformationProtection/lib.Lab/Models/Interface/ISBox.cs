using System.Collections;

namespace InformationProtection.lib.Lab.Models.Interface;

public interface ISBox
{
    public List<int> GetSsTransform(int index, List<int> part);
}