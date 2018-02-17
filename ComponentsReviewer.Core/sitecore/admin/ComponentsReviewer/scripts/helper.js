
define(["jquery"], function ($) {

    var helper = {

        isExist(name, list, contain) {
            var exists = false;
            $.each(list, function (i, sc) {
                if (!contain && sc == name) {
                    exists = true;
                    return;
                }
                if (contain && name.lastIndexOf(sc, 0) == 0) {
                    exists = true;
                    return;
                }
            });

            return exists;
        },

        stringToHtml(text) {
            if (!text || !text.split) { return "" }

            text = text.replace(/(?:\r\n|\r|\n)/g, ' <br /> ');

            var res = "";
            var spl = "";
            var words = text.split(" ");

            $.each(words, function (i, word) {
                if (word.startsWith("http:") || word.startsWith("https:")) {
                    word = "<a href='" + word + "' >" + word + "</a>";
                }
                res += spl + word;
                spl = " ";
            });

            return res;
        }
    }

    return helper;
});
