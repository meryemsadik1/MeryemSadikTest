using AventStack.ExtentReports;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MeryemSadik.Pages
{
    class ExtentTestManager
    {
        [ThreadStatic]
        private static ExtentTest _parentTest;

        [ThreadStatic]
        private static ExtentTest _childTest;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateParentTest()
        {
            _parentTest = ExtentManager.Instance.CreateTest(TestContext.CurrentContext.Test.Name);
            return _parentTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateTest()
        {
            _childTest = _parentTest.CreateNode(TestContext.CurrentContext.Test.Name);
            return _childTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest GetTest()
        {
            return _childTest;
        }
    }
}
