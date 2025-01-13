window.quillInterop = {
    quillInstances: {},

    initializeQuill: function (elementId, initialContent) {

        //if (this.quillInstances[elementId]) {
        //    return; // Quill is already initialized; do nothing
        //}

        const quill = new Quill(`#${elementId}`, {
            theme: 'snow'
        });

        // Set initial content
        quill.root.innerHTML = initialContent;

        // Store the instance
        this.quillInstances[elementId] = quill;

        // Event listener to update the Blazor component when content changes
        quill.on('text-change', () => {            
            DotNet.invokeMethodAsync("QuickAccounting", "UpdateQuillContentApart", elementId, quill.root.innerHTML);
        });
    },

    getQuillContent: function (elementId) {
        return this.quillInstances[elementId].root.innerHTML;
    }
};
