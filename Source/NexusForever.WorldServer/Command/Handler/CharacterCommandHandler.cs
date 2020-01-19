using System.Threading.Tasks;
using NexusForever.WorldServer.Command.Attributes;
using NexusForever.WorldServer.Command.Contexts;
using NexusForever.WorldServer.Game.Entity.Static;
using NexusForever.WorldServer.Game.Account.Static;
using NLog;

namespace NexusForever.WorldServer.Command.Handler
{
    [Name("Character", Permission.None)]
    public class CharacterCommandHandler : CommandCategory
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();
        public CharacterCommandHandler()
            : base(true, "character")
        {
        }

        [SubCommandHandler("addxp", "amount - Add the amount to your total xp.", Permission.CommandCharacterXp)]
        public Task AddXPCommand(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length > 0)
            {
                uint xp = uint.Parse(parameters[0]);

                if (context.Session.Player.Level < 50)
                    context.Session.Player.GrantXp(xp);
                else
                    context.SendMessageAsync("You must be less than max level.");
            }
            else
                context.SendMessageAsync("You must specify the amount of XP you wish to add.");

            return Task.CompletedTask;
        }

        // TODO: Update after "SetStat" packets are available.
        [SubCommandHandler("level", "value - Set your level to the value passed in", Permission.CommandCharacterLevel)]
        public Task SetLevelCommand(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length > 0)
            {
                byte level = byte.Parse(parameters[0]);

                if (context.Session.Player.Level < level && level <= 50)
                {
                    context.Session.Player.SetLevel(level);
                    context.SendMessageAsync($"Success! You are now level {level}.");
                }
                else
                    context.SendMessageAsync("Level must be more than your current level and no higher than level 50.");
            }
            else
            {
                context.SendMessageAsync("You must specify the level value you wish to assign.");
            }

            return Task.CompletedTask;
        }

        [SubCommandHandler("props", "[propertyname] [amount] - change character properties", Permission.CommandCharacterProperties)]
        public Task PropertiesSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            if (parameters.Length != 2)
            {
                context.SendMessageAsync("Available properties: speed, mountspeed, gravity, jump");
            }
            else
            {
                if (parameters[0].ToLower() == "speed")
                {
                    //change movement speed
                    float fValue = 0;
                    bool result = float.TryParse(parameters[1], out fValue);
                    if (result == true)
                    {
                        if ((fValue >= 1) & (fValue <= 8))
                        {
                            context.Session.Player.SetProperty(Property.MoveSpeedMultiplier, fValue, fValue);
                            log.Info($"{context.Session.Player.Name} : character props : speed {fValue}");
                        }
                        else
                        {
                            context.SendMessageAsync("You may not set a speed multiplier lower than 1 or higher than 8");
                            log.Info($"{context.Session.Player.Name} : character props : speed out of bounds");
                        }
                    }
                    else
                    {
                        context.SendMessageAsync("Invalid input: value must be a number between 1 and 8");
                        log.Info($"{context.Session.Player.Name} : character props : invalid input");
                    }
                }
                else if (parameters[0].ToLower() == "mountspeed")
                {
                    //change mount speed
                    float fValue = 0;
                    bool result = float.TryParse(parameters[1], out fValue);
                    if (result == true)
                    {
                        if ((fValue >= 1) & (fValue <= 5))
                        {
                            context.Session.Player.SetProperty(Property.MountSpeedMultiplier, fValue, fValue);
                            log.Info($"{context.Session.Player.Name} : character props : mountspeed {fValue}");
                        }
                        else
                        {
                            context.SendMessageAsync("You may not set a mount speed multiplier lower than 1 or higher than 5");
                            log.Info($"{context.Session.Player.Name} : character props : mountspeed out of bounds");
                        }
                    }
                    else
                    {
                        context.SendMessageAsync("Invalid input: value must be a number between 1 and 5");
                        log.Info($"{context.Session.Player.Name} : character props : invalid input");
                    }
                }
                else if (parameters[0].ToLower() == "gravity")
                {
                    //change gravity
                    float fValue = 0;
                    bool result = float.TryParse(parameters[1], out fValue);
                    if (result == true)
                    {
                        if ((fValue >= 0.1) & (fValue <= 5))
                        {
                            context.Session.Player.SetProperty(Property.GravityMultiplier, fValue, fValue);
                            log.Info($"{context.Session.Player.Name} : character props : gravity {fValue}");
                        }
                        else
                        {
                            context.SendMessageAsync("You may not set a gravity multiplier lower than 0.1 or higher than 5");
                            log.Info($"{context.Session.Player.Name} : character props : gravity out of bounds");
                        }
                    }
                    else
                    {
                        context.SendMessageAsync("Invalid input: value must be a number between 0.1 and 5");
                        log.Info($"{context.Session.Player.Name} : character props : invalid input");
                    }
                }
                else if (parameters[0].ToLower() == "jump")
                {
                    //change jump
                    float fValue = 0;
                    bool result = float.TryParse(parameters[1], out fValue);
                    if (result == true)
                    {
                        if ((fValue >= 1) & (fValue <= 20))
                        {
                            context.Session.Player.SetProperty(Property.JumpHeight, fValue, fValue);
                            log.Info($"{context.Session.Player.Name} : character props : jump {fValue}");
                        }
                        else
                        {
                            context.SendMessageAsync("You may not set a jump height lower than 1 or higher than 20");
                            log.Info($"{context.Session.Player.Name} : character props : jump out of bounds");
                        }
                    }
                    else
                    {
                        context.SendMessageAsync("Invalid input: value must be a number between 1 and 20");
                        log.Info($"{context.Session.Player.Name} : character props : invalid input");
                    }
                }

            }
            return Task.CompletedTask;
        }

        [SubCommandHandler("resetprops", "reset character properties to their defaults", Permission.CommandCharacterProperties)]
        public Task ResetPropertiesSubCommandHandler(CommandContext context, string command, string[] parameters)
        {
            context.Session.Player.SetProperty(Property.MoveSpeedMultiplier, 1f, 1f);
            context.Session.Player.SetProperty(Property.MountSpeedMultiplier, 2f, 2f);
            context.Session.Player.SetProperty(Property.GravityMultiplier, 1f, 1f);
            context.Session.Player.SetProperty(Property.JumpHeight, 5f, 5f);
            log.Info($"{context.Session.Player.Name} : character resetprops");
            return Task.CompletedTask;
        }
    }
}