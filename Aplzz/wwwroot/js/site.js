// function openDropDown(idPost) {
//     var posts = document.getElementsByClassName("dropDownMenuContent");
//     posts[idPost].style.display = "flex"
// }

// window.onclick = function(e) {
//     var posts = document.getElementsByClassName("dropDownMenuContent");
//     var ddBtn = document.getElementById("ddBtn");

// }

function openMoreBtn() {
    var navbarDD = document.getElementsByClassName("fixed_dropDownMenu_navbar")[0]
    if(navbarDD.style.display == "none" || navbarDD.style.display == "") {
        navbarDD.style.display = "flex"
    } else {
        navbarDD.style.display = "none"
    }
}

function openDDPostMenu(countNr) {
    var DDPostMenu = document.getElementsByClassName("dropDownMenuContent");
    if(DDPostMenu[countNr].style.display == "none" || DDPostMenu[countNr].style.display == "") {
        DDPostMenu[countNr].style.display = "flex"
    } else {
        DDPostMenu[countNr].style.display = "none"
    }
}

window.onclick = function(e) {
    var DDPostMenu = document.getElementsByClassName("dropDownMenuContent");
    var btnIdDD = document.getElementById("btnIdDD");
    if(e.target != document.getElementsByClassName("fixed_dropDownMenu_navbar")[0] && e.target != document.getElementById("functionClick")) {
        document.getElementsByClassName("fixed_dropDownMenu_navbar")[0].style.display = "none";
    }
    for (i = 0; i < DDPostMenu.length; i++) {
        if(e.target != DDPostMenu[i] && e.target != btnIdDD[i]) {
            DDPostMenu[i].style.display = "none";
        }
    }
}


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