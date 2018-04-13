using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Level
{
    public interface LevelGeneratorObserver
    {
        void OnNoLevelFilesFound();

        void OnLevelLoaded(Level level);
    }
}
