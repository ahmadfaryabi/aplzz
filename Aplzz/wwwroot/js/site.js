// function openDropDown(idPost) {
//     var posts = document.getElementsByClassName("dropDownMenuContent");
//     posts[idPost].style.display = "flex"
// }

// window.onclick = function(e) {
//     var posts = document.getElementsByClassName("dropDownMenuContent");
//     var ddBtn = document.getElementById("ddBtn");

// }
$(document).ready(function() {
    // Like button click event
    $(".like-button").click(function(e) {
        e.preventDefault();
        const button = $(this);
        const errorDiv = button.siblings('.like-error');
        const postId = button.data("post-id");
        
        button.prop('disabled', true);
        
        $.ajax({
            url: "/Post/LikePost",
            method: "POST",
            headers: {
                'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
            },
            data: { postId: postId },
            success: function(response) {
                button.find(".like-count").text(response.likesCount);
                button.toggleClass('liked', response.isLiked);
                
                const likeText = response.isLiked ? "Likt" : "Lik";
                button.find(".like-text").text(likeText);
                
                button.animate({ scale: 1.2 }, 100)
                     .animate({ scale: 1.0 }, 100);
                
                errorDiv.hide();
            },
            error: function(xhr) {
                errorDiv.text("Kunne ikke prosessere like. Prøv igjen senere.").show();
            },
            complete: function() {
                button.prop('disabled', false);
            }
        });
    });

    // Comment form submission
    $(".comment-form").submit(function(e) {
        e.preventDefault();
        const form = $(this);
        const postId = form.data("post-id");
        const input = form.find(".comment-input");
        const button = form.find("button");
        const spinner = button.find(".spinner-border");
        const errorDiv = form.siblings(".comment-error");
        const commentsContainer = form.closest(".card-body").find(".comments-container");

        const commentText = input.val().trim();
        if (!commentText) return;

        // Disable form and show loading state
        button.prop("disabled", true);
        spinner.removeClass("d-none");
        errorDiv.hide();

        $.ajax({
            url: "/Post/AddComment",
            method: "POST",
            headers: {
                'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
            },
            data: { postId: postId, commentText: commentText },
            success: function(response) {
                // Add new comment to UI
                const newComment = `
                    <div class="border-top pt-2">
                        <p>${response.text}</p>
                        <small class="text-muted">${response.commentedAt}</small>
                    </div>
                `;
                commentsContainer.prepend(newComment);
                
                // Clear input and reset form
                input.val("");
                form.closest(".collapse").collapse("hide");
            },
            error: function(xhr) {
                const error = xhr.responseJSON?.error || "Kunne ikke legge til kommentar. Prøv igjen senere.";
                errorDiv.text(error).show();
            },
            complete: function() {
                button.prop("disabled", false);
                spinner.addClass("d-none");
            }
        });
    });
});