// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using uniffi.uniffi_docstring;

namespace UniffiCS.BindingTests;

public class TestDocstring
{
    class CallbackImpls : CallbackTest
    {
        public void Test() { }
    }

    [Fact]
    public void DocstringWorks()
    {
        // Checking to make sure the symbols are reachable,
        // not accidentally commented out by docstrings

        UniffiDocstringMethods.Test();

        _ = EnumTest.One;
        _ = EnumTest.Two;

        _ = new AssociatedEnumTest.Test(0);
        _ = new AssociatedEnumTest.Test2(0);

        _ = new ErrorTest.One("hello");
        _ = new ErrorTest.Two("hello");

        _ = new AssociatedErrorTest.Test(0);
        _ = new AssociatedErrorTest.Test2(0);

        var obj1 = new ObjectTest();
        var obj2 = ObjectTest.NewAlternate();
        obj2.Test();

        var record = new RecordTest(123);
        _ = record.test;

        CallbackTest callback = new CallbackImpls();
        callback.Test();
    }

    [Fact]
    public void DocstringsAppearInBindings()
    {
        // // Hacky way to find project directory based on working directory..
        string rootDirectory = Directory.GetCurrentDirectory() + "../../../../../../";
        string bindingsSource = File.ReadAllText(rootDirectory + "dotnet-tests/UniffiCS/gen/uniffi_docstring.cs");

        List<string> expected = new List<string> {
            "<docstring-alternate-constructor>",
            "<docstring-associated-enum-variant-2>",
            "<docstring-associated-enum-variant>",
            "<docstring-associated-enum>",
            "<docstring-associated-error-variant-2>",
            "<docstring-associated-error-variant>",
            "<docstring-associated-error>",
            "<docstring-callback-method>",
            "<docstring-callback>",
            "<docstring-enum-variant-2>",
            "<docstring-enum-variant>",
            "<docstring-enum>",
            "<docstring-error-variant-2>",
            "<docstring-error-variant>",
            "<docstring-error>",
            "<docstring-function>",
            "<docstring-method>",
            "<docstring-namespace>",
            "<docstring-object>",
            "<docstring-primary-constructor>",
            "<docstring-record-field>",
            "<docstring-record>"
        };

        List<string> missingDocstrings = expected.Where(e => !bindingsSource.Contains(e)).ToList();
        Assert.Empty(missingDocstrings);
    }
}
