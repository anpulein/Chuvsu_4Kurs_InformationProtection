using System.Collections;

namespace InformationProtection.lib.Lab.Models.Interface;

public interface IDES
{
    public List<int> EncodeBlock(List<int> message);
    public List<int> DecodeBlock(List<int> message);
}