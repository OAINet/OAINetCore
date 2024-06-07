using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace OAINet.Node.Blockchain
{
    public class Blockchain : IBlockchain
    {
        private readonly ILogger<Blockchain> _logger;
        private List<Block> _blockchain;
        private List<Block> _pendingBlocks;

        public Blockchain(ILogger<Blockchain> logger)
        {
            _logger = logger;
            _blockchain = new List<Block>();
            _pendingBlocks = new List<Block>();
            StartSaveTimer();
        }

        public Block AddBlock(Block block)
        {
            var lastBlock = GetStaticBlockchain().LastOrDefault();
            
            block.PreviousHash = lastBlock == null ? "0" : lastBlock.Hash;

            block.Hash = block.CalculateHash();
            _pendingBlocks.Add(block);
            return block;
        }

        public bool CheckIntegrity(List<Block> blockSample)
        {
            throw new NotImplementedException();
        }

        public List<Block> GetStaticBlockchain()
        {
            return LoadBlockchain();
        }

        public List<Block> GetStaticBlockchain(int indexStart, int indexStop)
        {
            throw new NotImplementedException();
        }

        private void SaveBlockchain()
        {
            try
            {
                var number = _pendingBlocks.Count;
                _logger.LogInformation($"{number} blocks are going to be included in the local blockchain.");

                _blockchain.AddRange(_pendingBlocks);
                _pendingBlocks.Clear();

                using (var stream = new FileStream("blockchain.bin", FileMode.Create))
                using (var writer = new BinaryWriter(stream))
                {
                    
                    foreach (var block in _blockchain)
                    {
                        writer.Write(block.Hash);
                        writer.Write(block.PreviousHash);
                        writer.Write(block.CreatedAt.Ticks);
                        writer.Write(JsonSerializer.Serialize(block.Content));
                        
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving blockchain: {ex.Message}");
            }
        }

        private List<Block> LoadBlockchain()
        {
            var blockchain = new List<Block>();

            try
            {
                if (File.Exists("blockchain.bin"))
                {
                    using (var stream = new FileStream("blockchain.bin", FileMode.Open))
                    using (var reader = new BinaryReader(stream))
                    {
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            var block = new Block
                            {
                                Hash = reader.ReadString(),
                                PreviousHash = reader.ReadString(),
                                CreatedAt = new DateTime(reader.ReadInt64()),
                                Content = JsonSerializer.Deserialize<BaseContentType>(reader.ReadString())
                            };
                            blockchain.Add(block);
                        }
                    }
                }
                else
                {
                    _logger.LogError("Blockchain file not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading blockchain: {ex.Message}");
            }

            return blockchain;
        }

        private void StartSaveTimer()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(TimeSpan.FromSeconds(5));
                    SaveBlockchain();
                }
            });
        }
    }

    public interface IBlockchain
    {
        Block AddBlock(Block block);
        bool CheckIntegrity(List<Block> blockSample);
        List<Block> GetStaticBlockchain();
        List<Block> GetStaticBlockchain(int indexStart, int indexStop);
    }
}