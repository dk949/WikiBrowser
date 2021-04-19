namespace WikiBrowser.Requests {
    internal struct Result {
        public string Title { get; set; }
        public string Body { get; set; }

        public Result(string title, string body) {
            Title = title;
            Body = body;
        }

        public Result(string title) {
            Title = title;
            Body = "";
        }
    }
}