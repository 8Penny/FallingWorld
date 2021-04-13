using System.Collections.Generic;

namespace Foundation {
    public interface IOnInventoryCellUpdated {
        void Do(List<int> cellIndices);
    }
}