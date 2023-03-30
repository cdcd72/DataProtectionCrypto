using DataProtectionCrypto.Commands;

var console = ConsoleApp.Create(args);

console.AddCommands<DataProtectionCommand>();

console.Run();