$(document).ready(function () {
    tinymce.init({
        selector: 'textarea',
        plugins: 'autoresize anchor autolink charmap codesample emoticons image link lists media searchreplace table visualblocks wordcount',
        autoresize_min_height: 200,
        toolbar: 'undo redo | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent'
    });

    const tagsContainer = $("#tag-container");
    const tagsInput = $("#tags-input");
    const tagsData = [];

    tagsData.forEach(function (tag) {
        const tagElement = $("<div class='tag'>" + tag + "</div>");
        tagsContainer.append(tagElement);
    });

    $("#add-tag").click(function () {
        const tagInput = $("#tag-input");
        const tagValue = tagInput.val().trim();


        if (tagValue !== "" && tagsData.indexOf(tagValue) === -1) {
            const tagElement = $("<div class='tag'>" + tagValue + "</div>");
            tagsContainer.append(tagElement);
            tagInput.val("");
            tagsData.push(tagValue);
            tagsInput.val(tagsData.join(','));
        }
        else if (tagsData.indexOf(tagValue) !== -1){
            alert(`You have already entered "${tagValue}" tag! They must be unique.`)
        }
    });

    tagsContainer.on("click", ".tag", function () {
        const tagValue = $(this).text().trim();
        const tagIndex = tagsData.indexOf(tagValue);

        if (tagIndex !== -1) {
            tagsData.splice(tagIndex, 1);
            tagsInput.val(tagsData.join(','));
        }

        $(this).remove();
    });
});




