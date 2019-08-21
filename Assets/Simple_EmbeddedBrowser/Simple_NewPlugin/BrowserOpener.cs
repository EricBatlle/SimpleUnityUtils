public class BrowserOpener : Singleton<BrowserOpener>
{
    private string pageToOpen = "https://www.google.com/";

    // check readme file to find out how to change title, colors etc.
    public void OnButtonClicked(string pageToOpen)
    {
        this.pageToOpen = pageToOpen;
		InAppBrowser.DisplayOptions options = new InAppBrowser.DisplayOptions();
		options.displayURLAsPageTitle = false;
		options.pageTitle = "PAGE TITLE";
        options.backButtonText = "<";
        options.backButtonFontSize = "30";
        options.barBackgroundColor = "#B10034";
        options.textColor = "#FFFFFF";
        /*
        int paddingTop = 100;
        int paddingBottom = 100;
        int paddingLeft = 100;
        int paddingRight = 100;
        InAppBrowser.EdgeInsets insets = new InAppBrowser.EdgeInsets(paddingTop,
        paddingBottom, paddingLeft, paddingRight);
        options.insets = insets;
        */
        InAppBrowser.OpenURL(pageToOpen, options);
        print("Opened browser in: "+pageToOpen);
	}

    public void OnButtonClicked(string pageToOpen, string pageTitle)
    {
        this.pageToOpen = pageToOpen;
        InAppBrowser.DisplayOptions options = new InAppBrowser.DisplayOptions();
        options.displayURLAsPageTitle = false;
        options.pageTitle = pageTitle;
        options.backButtonText = "<";
        options.backButtonFontSize = "30";
        options.barBackgroundColor = "#B10034";
        options.textColor = "#FFFFFF";
        /*
        int paddingTop = 100;
        int paddingBottom = 100;
        int paddingLeft = 100;
        int paddingRight = 100;
        InAppBrowser.EdgeInsets insets = new InAppBrowser.EdgeInsets(paddingTop,
        paddingBottom, paddingLeft, paddingRight);
        options.insets = insets;
        */
        InAppBrowser.OpenURL(pageToOpen, options);
        print("Opened browser in: " + pageToOpen);
    }

    public void OnClearCacheClicked()
    {
		InAppBrowser.ClearCache();
	}
}
