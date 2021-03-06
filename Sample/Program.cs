﻿using System;
using System.Text;
using System.Threading.Tasks;
using MapleFedNet.Api;
using MapleFedNet.Common;
using MapleFedNet.Model;

namespace Sample
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            const string domain = "mstdn.jp";
            const string clientName = "Mastodon.Net";
            Console.WriteLine("User name:");
            var userName = Console.ReadLine();
            Console.WriteLine("Password:");
            var password = GetConsolePassword();

            var oauth = await Apps.Register(domain, clientName,
                scopes: new[] {Scope.Follow, Scope.Read, Scope.Write});
            var token = await OAuth.GetAccessTokenByPassword(domain, oauth.ClientId, oauth.ClientSecret,
                userName, password, Scope.Follow, Scope.Read, Scope.Write);

			var credentials = new MastodonCredentials(domain, token.AccessToken);
            var verify = await Accounts.VerifyCredentials(credentials);
            var timeline = await Timelines.Home(credentials);
            var notify = await Notifications.Fetching(credentials);
            var toot = await Statuses.Posting(credentials, "Toot!");
            Console.WriteLine("complete!");
        }

        private static string GetConsolePassword()
        {
            var sb = new StringBuilder();
            while (true)
            {
                var cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }

                if (cki.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                    {
                        Console.Write("\b\0\b");
                        sb.Length--;
                    }

                    continue;
                }

                Console.Write('*');
                sb.Append(cki.KeyChar);
            }

            return sb.ToString();
        }
    }
}