using System;

namespace Tests
{
    class HomeworkMethod
    {
        public bool IsInputEven(int x)
        {
            bool isEven;

            if(x % 2 == 0)
            {
                isEven = true;
            }
            else
            {
                isEven = false;
            }

            return isEven;
        }
    }
}