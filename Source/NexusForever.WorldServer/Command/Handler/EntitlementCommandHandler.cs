using System.Threading.Tasks;
using NexusForever.Shared.GameTable;
using NexusForever.WorldServer.Command.Attributes;
using NexusForever.WorldServer.Command.Contexts;
using NexusForever.WorldServer.Game.Entity;
using NexusForever.WorldServer.Game.Entity.Static;
using NexusForever.WorldServer.Game.Account.Static;
using NLog;

namespace NexusForever.WorldServer.Command.Handler
{
    [Name("Entitlement", Permission.None)]
    public class EntitlementCommandHandler : CommandCategory
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();
        public EntitlementCommandHandler()
            : base(true, "entitlement")
        {
        }

        [SubCommandHandler("account", "entitlementId amount - Create or update account entitlement.", Permission.CommandEntitlement)]
        public async Task EntitlementAccountCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 2
                || !uint.TryParse(parameters[0], out uint entitlementId)
                || !int.TryParse(parameters[1], out int value))
            {
                await SendHelpAsync(context);
                return;
            }

            if (GameTableManager.Entitlement.GetEntry(entitlementId) == null)
            {
                log.Info($"{context.Session.Player.Name} : entitlement account : invalid entitlement ID : {entitlementId}");
                await context.SendMessageAsync($"{entitlementId} isn't a valid entitlement id!");
                return;
            }

            context.Session.EntitlementManager.SetAccountEntitlement((EntitlementType)entitlementId, value);
        }

        [SubCommandHandler("character", "entitlementId amount - Create or update character entitlement.", Permission.CommandEntitlement)]
        public async Task EntitlementCharacterCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 2
                || !uint.TryParse(parameters[0], out uint entitlementId)
                || !int.TryParse(parameters[1], out int value))
            {
                await SendHelpAsync(context);
                return;
            }

            if (GameTableManager.Entitlement.GetEntry(entitlementId) == null)
            {
                log.Info($"{context.Session.Player.Name} : entitlement character : invalid entitlement ID : {entitlementId}");
                await context.SendMessageAsync($"{entitlementId} isn't a valid entitlement id!");
                return;
            }

            context.Session.EntitlementManager.SetCharacterEntitlement((EntitlementType)entitlementId, value);
        }

        [SubCommandHandler("list", "List all entitlements for account and character.", Permission.CommandEntitlement)]
        public async Task EntitlementListCommandHandler(CommandContext context, string command, string[] parameters)
        {
            await context.SendMessageAsync($"Entitlements for account {context.Session.Account.Id}:");
            foreach (AccountEntitlement entitlement in context.Session.EntitlementManager.GetAccountEntitlements())
                await context.SendMessageAsync($"Entitlement: {entitlement.Type}, Value: {entitlement.Amount}");

            await context.SendMessageAsync($"Entitlements for character {context.Session.Player.CharacterId}:");
            foreach (CharacterEntitlement entitlement in context.Session.EntitlementManager.GetCharacterEntitlements())
                await context.SendMessageAsync($"Entitlement: {entitlement.Type}, Value: {entitlement.Amount}");
        }
    }
}