using ShopeOnline.Test;

namespace ShopOnline.Test
{
    using FluentAssertions;

    using Microsoft.Playwright;

    [Collection(PlaywrightFixture.PlaywrightCollection)]
    public class MyTestClass
    {
        private readonly PlaywrightFixture playwrightFixture;
        /// <summary>
        /// Setup test class injecting a playwrightFixture instance.
        /// </summary>
        /// <param name="playwrightFixture">The playwrightFixture
        /// instance.</param>
        public MyTestClass(PlaywrightFixture playwrightFixture)
        {
            this.playwrightFixture = playwrightFixture;
        }

        [Fact]
        public async Task MyFirstTest()
        {
            var url = "https://localhost:7102";
            // Open a page and run test logic.
            await this.playwrightFixture.GotoPageAsync(
                url,
                async (page) =>
                    {
                        // Click text=Home
                        await page.Locator("text=Home").ClickAsync();
                        await page.WaitForURLAsync($"{url}/");
                        // Click text=Hello, world!
                        await page.Locator("text=ShopOnline").IsVisibleAsync();

                        // Click text=Counter
                        await page.GetByRole(AriaRole.Link, new() { NameString = "Cart"}).ClickAsync();
                        await page.WaitForURLAsync($"{url}/ShoppingCart");
                        // Click h1:has-text("Counter")

                        await page.PauseAsync();
                        await page.Locator("h3:has-text(\"Shopping Cart\")").IsVisibleAsync();
                        // Click text=Click me
                        var images = page.GetByRole(AriaRole.Img);
                        (await images.CountAsync()).Should().Be(2);
                    },
                Browser.Chromium);
        }
    }
}