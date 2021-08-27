using System;

namespace func.brainfuck
{
    public class VirtualMachineBuilder
    {
        private Func<string, IVirtualMachine> factory;

        public VirtualMachineBuilder(int memorySize)
        {
            factory = instructions => new VirtualMachine(instructions, memorySize);
        }

        public VirtualMachineBuilder AddBasicCommands(Func<int> read, Action<char> write)
        {
            var previousFactory = factory;
            factory = instructions =>
            {
                var vm = previousFactory(instructions);
                BrainfuckBasicCommands.RegisterTo(vm, read, write);
                return vm;
            };
            return this;
        }

        public VirtualMachineBuilder AddLoop()
        {
            var previousFactory = factory;
            factory = instructions =>
            {
                var vm = previousFactory(instructions);
                BrainfuckLoopCommands.RegisterTo(vm);
                return vm;
            };
            return this;
        }

        public IVirtualMachine Build(string instructions) => factory(instructions);
    }
}