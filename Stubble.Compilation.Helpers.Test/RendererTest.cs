using System.Globalization;
using McMaster.Extensions.Xunit;
using Stubble.Compilation.Settings;
using Xunit;

namespace Stubble.Compilation.Helpers.Test
{
    public class RendererTest
	{
        [Fact]
        [UseCulture("en-GB")]
        public void StubbleShouldContinueWorkingAsNormal()
        {
            var culture = new CultureInfo("en-GB");
            var helpers = new CompiledHelpers()
                .Register<decimal>("FormatCurrency", (context, count) => count.ToString("C", culture));

            var settings = new CompilerSettingsBuilder()
                .AddHelpers(helpers)
                .BuildSettings();

            var stubble = new StubbleCompilationRenderer(settings);

            var tmpl = @"{{Count}}: {{FormatCurrency Count}}, {{Count2}}: {{FormatCurrency Count2}}";
            var data = new { Count = 10m, Count2 = 100.26m };
            var func = stubble.Compile(tmpl, data);

            var res = func(data);

            Assert.Equal("10: £10.00, 100.26: £100.26", res);
        }

        public class FakeTemplate
        {
            public FakeDataObject Person { get; set; }
        }

        public class FakeDataObject
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [Fact]
        [UseCulture("en-GB")]
        public void StubbleTemplateRendering()
        {
            var culture = new CultureInfo("en-GB");
            var helpers = new CompiledHelpers()
                .Register<FakeDataObject>("FormatPerson", (context, person) => $"{person.FirstName} {person.LastName}");

            var settings = new CompilerSettingsBuilder()
                .AddHelpers(helpers)
                .BuildSettings();

            var stubble = new StubbleCompilationRenderer(settings);

            var tmpl = @"Hello, {{FormatPerson Person}}.";
            var data = new FakeTemplate
            {
                Person = new FakeDataObject
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                }
            };
            var func = stubble.Compile(tmpl, data);

            var res = func(data);

            Assert.Equal("Hello, Jane Doe.", res);
        }
	}
}
