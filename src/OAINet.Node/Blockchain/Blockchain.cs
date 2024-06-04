using System.Runtime.Serialization.Formatters.Binary;

namespace OAINet.Node.Blockchain;


public class Blockchain : IBlockchain
{
    private List<Block> pucher = new List<Block>();
    public Block AddBlock(Block block)
    {
        throw new NotImplementedException();
    }

    public bool CheckIntegrity(List<Block> blockSample)
    {
        throw new NotImplementedException();
    }
    
    private void SaveBlockchain()
    {
        if (File.Exists("blockchain.bin"))
        {
            // TODO: Save the blockchain with new block in the pusher
            return;  
        }

        var safetyBlockchain = RebootBlockchain();
        using (var stream = File.Create("blockchain.bin"))
        {
            BinaryWriter binaryWriter = new BinaryWriter(stream);
            binaryWriter.Write(safetyBlockchain);
        }
    }

    private byte[] RebootBlockchain()
    {
        throw new NotImplementedException();
    }

    private Block CreateGenesisBlock()
    {
        return new Block()
        {
            PreviousHash = "0",
            Content = new SimpleContentType()
            {
                OwnerPK = "0"
            },
            CreatedAt = DateTime.Now
        };
    }
}

public interface IBlockchain
{
    public Block AddBlock(Block block);
    public bool CheckIntegrity(List<Block> blockSample);
}