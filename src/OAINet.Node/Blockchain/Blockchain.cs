namespace OAINet.Node.Blockchain;


public class Blockchain : IBlockchain
{
    public Block AddBlock(Block block)
    {
        throw new NotImplementedException();
    }

    public bool CheckIntegrity(List<Block> blockSample)
    {
        throw new NotImplementedException();
    }
}

public interface IBlockchain
{
    public Block AddBlock(Block block);
    public bool CheckIntegrity(List<Block> blockSample);
}