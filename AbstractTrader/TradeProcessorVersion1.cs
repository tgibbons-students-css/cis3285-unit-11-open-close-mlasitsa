using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AbstractTrader
{
    public class TradeProcessorVersion1 : ITradeProcessor
    {
        public void ProcessTrades(Stream stream)
        {
            LogMessage("INFO: Processing trades in Version1");
            var lines = ReadTradeData(stream);
            var trades = ParseTrades(lines);
            StoreTrades(trades);
        }

        private void LogMessage(string message, params object[] args)
        {
            Console.WriteLine(message, args);
            using (StreamWriter logfile = File.AppendText("log.xml"))
            {
                logfile.WriteLine("<log>" + message + "</log>", args);
            }
        }

        protected IEnumerable<string> ReadTradeData(Stream stream)
        {
            LogMessage("INFO: ReadTradeData version 1");
            var tradeData = new List<string>();
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    tradeData.Add(line);
                }
            }
            return tradeData;
        }

        protected IEnumerable<TradeRecord> ParseTrades(IEnumerable<string> tradeData)
        {
            LogMessage("INFO: ParseTrades version 1");
            var trades = new List<TradeRecord>();
            var lineCount = 1;
            foreach (var line in tradeData)
            {
                var fields = line.Split(new char[] { ',' });

                var trade = MapTradeDataToTradeRecord(fields);

                trades.Add(trade);

                lineCount++;
            }

            return trades;
        }

        protected TradeRecord MapTradeDataToTradeRecord(string[] fields)
        {
            var sourceCurrencyCode = fields[0].Substring(0, 3);
            var destinationCurrencyCode = fields[0].Substring(3, 3);
            var tradeAmount = int.Parse(fields[1]);
            var tradePrice = decimal.Parse(fields[2]);
            const float LotSize = 100000f;
            var trade = new TradeRecord
            {
                SourceCurrency = sourceCurrencyCode,
                DestinationCurrency = destinationCurrencyCode,
                Lots = tradeAmount / LotSize,
                Price = tradePrice
            };

            return trade;
        }

        protected void StoreTrades(IEnumerable<TradeRecord> trades)
        {
            LogMessage("INFO: Simulating database connection in StoreTrades");
            LogMessage("INFO: {0} trades processed", trades.Count());
        }
    }
}
