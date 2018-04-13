using Assets.Level.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Level
{
    public class CheckpointManager
    {

        private static readonly List<CheckpointBlock> CHECKPOINTS = new List<CheckpointBlock>();

        public static CheckpointBlock ActiveCheckpoint { get; set; }

        public static void AddCheckpoint(CheckpointBlock checkpoint)
        {
            CHECKPOINTS.Add(checkpoint);
            if (ActiveCheckpoint == null)
                ActiveCheckpoint = checkpoint;
        }

        public static void RemoveCheckpoint(CheckpointBlock checkpoint)
        {
            CHECKPOINTS.Remove(checkpoint);
        }

        internal static void ClearCheckpoints()
        {
            CHECKPOINTS.Clear();
        }
    }
}
