// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using uniffi.custom_types;

namespace UniffiCS.BindingTests;

public class TestCustomTypes
{
    [Fact]
    public void CustomTypesWork()
    {
        var demo = CustomTypesMethods.GetCustomTypesDemo(null);

        Assert.Equal("http://example.com/", demo.url);
        Assert.Equal(123, demo.handle);

        // Change some data and ensure that the round-trip works
        demo = demo with
        {
            url = "http://new.example.com/"
        };
        demo = demo with { handle = 456 };
        Assert.Equal(demo, CustomTypesMethods.GetCustomTypesDemo(demo));
    }
}
