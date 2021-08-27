using System;
using System.Collections.Generic;
using System.Linq;

namespace func.brainfuck
{
    public class BrainfuckBasicCommands
    {
        private static IEnumerable<char> constants = Range('0', '9')
            .Concat(Range('a', 'z'))
            .Concat(Range('A', 'Z'));
        
        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            FillConstants(vm);
			
            vm.RegisterCommand('.', b
                => write((char)vm.Memory[vm.MemoryPointer]));
            vm.RegisterCommand(',', b
                => vm.Memory[vm.MemoryPointer] = (byte)read());
			
            vm.RegisterCommand('+', b
                => vm.Memory[vm.MemoryPointer] = (byte)ModularSum(vm.Memory[vm.MemoryPointer], 1, 256) );
            vm.RegisterCommand('-', b
                => vm.Memory[vm.MemoryPointer] = (byte)ModularSum(vm.Memory[vm.MemoryPointer], -1, 256) );
			
            vm.RegisterCommand('>', b 
                => vm.MemoryPointer = ModularSum(vm.MemoryPointer, 1, vm.Memory.Length));
            vm.RegisterCommand('<', b 
                => vm.MemoryPointer = ModularSum(vm.MemoryPointer, -1, vm.Memory.Length));
        }
		
        private static int ModularSum(int s1, int s2, int modulo)
            => (((s1 % modulo) + (s2 % modulo) + modulo) % modulo);

        private static IEnumerable<char> Range(char first, char last)
        {
            for (var i = first; i <= last; i++)
                yield return i;
        }

        private static void FillConstants(IVirtualMachine vm)
        {	
            foreach (var c in constants)
                vm.RegisterCommand(c, b => b.Memory[b.MemoryPointer] = (byte)c);
        }
    }
}