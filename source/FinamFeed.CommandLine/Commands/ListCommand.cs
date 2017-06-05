﻿namespace FinamFeed.CommandLine.Commands
{
    using System.IO;
    using System.Threading.Tasks;
    using FinamFeed.CommandLine.Options;

    public class ListCommand : CommandBase<ListCommandOptions>
    {
        public ListCommand(ListCommandOptions options, TextWriter output, TextWriter error) : base(options, output, error)
        {
        }

        protected override Task ProcessInternal()
        {
            if (this.Options.Markets)
            {
                var markets = this.FeedApi.Repository.GetAllMarkets();
                this.View.DisplayMarkets(markets);
            }
            else if (this.Options.Symbols)
            {
                var symbols = this.FeedApi.Repository.GetAllSymbols();
                this.View.DisplaySymbols(symbols, this.FeedApi.Repository.GetMarketsDictionary());
            }

            return Task.FromResult(0);
        }

        public override bool ValidateOptions()
        {
            return base.ValidateOptions() ? this.Options.IsValid() : false;
        }

        public override string GetUsage()
        {
            return this.Options.GetUsage();
        }
    }
}
