﻿namespace FinamFeed.CommandLine.Commands
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using FinamFeed.Api;

    public abstract class CommandBase<TOptions> : ICommand
    {
        protected FeedApi FeedApi { get; private set; }
        protected ViewHelper View { get; private set; }

        protected TOptions Options { get; private set; }
        protected TextWriter Output { get; private set; }
        protected TextWriter Error { get; private set; }

        protected CommandBase(TOptions options, TextWriter output, TextWriter error)
        {
            this.Options = options;
            this.Output = output;
            this.Error = error;
            this.View = new ViewHelper(this.Output, this.Error);
            this.FeedApi = new FeedApi(new ApiConfiguration());
        }

        public async Task Process()
        {
            try
            {
                await this.ProcessInternal().ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                this.Error.WriteLine($"ERROR: {ex}");
            }
        }

        protected virtual Task ProcessInternal()
        {
            return Task.FromResult(0);
        }

        public virtual bool ValidateOptions()
        {
            if (this.Options == null)
            {
                return false;
            }

            return true;

        }

        public virtual string GetUsage()
        {
            return string.Empty;
        }
    }
}
