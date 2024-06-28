using System.Security.Cryptography;
using System.Text;

namespace OAINet.Blockchain.Store.Logics;

public class Header
{
    public int Version { get; set; }
    public string PreviousHash { get; set;  }
    public DateTime CreatedAt { get; set; }
    public int DifficultyTarget { get; set; }
}