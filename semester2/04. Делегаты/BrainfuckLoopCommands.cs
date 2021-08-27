using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace func.brainfuck
{
    public class BrainfuckLoopCommands
    {
        public static void RegisterTo(IVirtualMachine vm)
        {
            var bracketIndexes = MakeBracketIndexes(vm.Instructions);
            
            vm.RegisterCommand('[', b =>
            {
                if (b.Memory[b.MemoryPointer] == 0)
                    b.InstructionPointer = bracketIndexes[b.InstructionPointer];
            });
            vm.RegisterCommand(']', b =>
            {
                if (b.Memory[b.MemoryPointer] != 0)
                    b.InstructionPointer = bracketIndexes[b.InstructionPointer];
            });
        }

        private static Dictionary<int, int> MakeBracketIndexes(string instructions)
        {
            var stack = new Stack<int>();
            var result = new Dictionary<int, int>();
            for (var i = 0; i < instructions.Length; i++)
                switch (instructions[i])
                {
                    case '[':
                        stack.Push(i);
                        break;
                    case ']':
                        result[i] = stack.Pop();
                        result[result[i]] = i;
                        break;
                }

            return result;
        }
    }
}