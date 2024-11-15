using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AbstractTrader
{
    public class TradeProcessorVersion2 : ITradeProcessor
    {
        public void ProcessTrades(Stream stream)
        {
            LogMessage("INFO: Processing trades in Version2");
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
            var tradeData = new List<string>();
            LogMessage("INFO: Simulating ReadTradeData version 2");
            return tradeData;
        }

        protected IEnumerable<TradeRecord> ParseTrades(IEnumerable<string> tradeData)
        {
            var trades = new List<TradeRecord>();
            LogMessage("INFO: Simulating ParseTrades version 2");
            return trades;
        }

        protected void StoreTrades(IEnumerable<TradeRecord> trades)
        {
            LogMessage("INFO: Simulating database connection in StoreTrades version 2");
            LogMessage("INFO: {0} trades processed", trades.Count());
        }
    }
}
