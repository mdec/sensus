﻿// Copyright 2014 The Rector & Visitors of the University of Virginia
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using SensusService.Anonymization;
using SensusService.Anonymization.Anonymizers;
using System.Collections.Generic;

namespace SensusService.Probes.Apps
{
    public class FacebookDatum : Datum
    {
        // below are the various permissions and the fields/edges that they provide access to. the
        // list is taken from https://developers.facebook.com/docs/facebook-login/permissions/v2.3#reference

        // user object fields
        [Anonymizable("Age Range:", typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("public_profile", new string[0], "age_range")]
        public string AgeRange { get; set; }

        [Anonymizable("First Name:", typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("public_profile", new string[0], "first_name")]
        public string FirstName { get; set; }

        [Anonymizable(null, typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("public_profile", new string[0], "gender")]
        public string Gender { get; set; }

        [Anonymizable("User ID:", typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("public_profile", new string[0], "id")]
        public new string Id { get; set; }

        [Anonymizable("Last Name:", typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("public_profile", new string[0], "last_name")]
        public string LastName { get; set; }

        [Anonymizable("Link to Timeline:", typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("public_profile", new string[0], "link")]
        public string TimelineLink { get; set; }

        [Anonymizable(null, typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("public_profile", new string[0], "locale")]
        public string Locale { get; set; }

        [Anonymizable("Full Name:", typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("public_profile", new string[0], "name")]
        public string FullName { get; set; }

        [Anonymizable(null, typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("public_profile", new string[0], "timezone")]
        public string Timezone { get; set; }

        [Anonymizable("Time of Last Update:", typeof(DateTimeOffsetTimelineAnonymizer), true)]
        [FacebookPermission("public_profile", new string[0], "updated_time")]
        public DateTimeOffset UpdatedTime { get; set; }

        [Anonymizable("Whether User is Verified:", null, true)]
        [FacebookPermission("public_profile", new string[0], "verified")]
        public bool Verified { get; set; }

        [Anonymizable("Email Address:", typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("email", new string[0], "email")]
        public string Email { get; set; }

        [Anonymizable("Biography:", typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_about_me", new string[0], "bio")]
        public string Biography { get; set; }

        [Anonymizable(null, typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_education_history", new string[0], "education")]
        public List<string> Education { get; set; }

        [Anonymizable(null, typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_hometown", new string[0], "hometown")]
        public string Hometown { get; set; }

        [Anonymizable(null, typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_location", new string[0], "location")]
        public string Location { get; set; }

        [Anonymizable("Relationship Status:", typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_relationships", new string[0], "relationship_status")]
        public string RelationshipStatus { get; set; }

        [Anonymizable("Significant Other:", typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_relationships", new string[0], "significant_other")]
        public string SignificantOther { get; set; }

        [Anonymizable(null, typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_religion_politics", new string[0], "religion")]
        public string Religion { get; set; }

        [Anonymizable(null, typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_religion_politics", new string[0], "political")]
        public string Politics { get; set; }

        [Anonymizable(null, typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_website", new string[0], "website")]
        public string Website { get; set; }

        [Anonymizable("Employment History:", typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_work_history", new string[0], "work")]
        public List<string> Employment { get; set; }

        // user edges
        [Anonymizable(null, typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_friends", "friends", new string[0])]
        public List<string> Friends { get; set; }

        [Anonymizable("Books Read:", typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_actions.books", "book.reads", new string[0])]
        public List<string> Books { get; set; }

        [Anonymizable(null, typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_actions.fitness", "fitness.runs", new string[0])]
        public List<string> Runs { get; set; }

        [Anonymizable(null, typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_actions.fitness", "fitness.walks", new string[0])]
        public List<string> Walks { get; set; }

        [Anonymizable("Bike Rides:", typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_actions.fitness", "fitness.bikes", new string[0])]
        public List<string> Bikes { get; set; }

        [Anonymizable("Songs Listened To:", typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_actions.music", "music.listens", new string[0])]
        public List<string> Songs { get; set; }

        [Anonymizable(null, typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_actions.music", "music.playlists", new string[0])]
        public List<string> Playlists { get; set; }

        [Anonymizable("News Read:", typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_actions.news", "news.reads", new string[0])]
        public List<string> NewsReads { get; set; }

        [Anonymizable("News Published:", typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_actions.news", "news.publishes", new string[0])]
        public List<string> NewsPublishes { get; set; }

        [Anonymizable("Videos Watched:", typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_actions.video", "video.watches", new string[0])]
        public List<string> VideosWatched { get; set; }

        [Anonymizable("Video Ratings:", typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_actions.video", "video.rates", new string[0])]
        public List<string> VideoRatings { get; set; }

        [Anonymizable("Video Wish List:", typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_actions.video", "video.wants_to_watch", new string[0])]
        public List<string> VideoWishList { get; set; }

        [Anonymizable(null, typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_events", "events", new string[0])]
        public List<string> Events { get; set; }

        [Anonymizable(null, typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_games_activity", "games", new string[0])]
        public List<string> Games { get; set; }

        [Anonymizable(null, typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_groups", "groups", new string[0])]
        public List<string> Groups { get; set; }

        [Anonymizable(null, typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_likes", "likes", new string[0])]
        public List<string> Likes { get; set; }

        [Anonymizable("Captions of Posted Photos:", typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_photos", "photos", new string[0])]
        public List<string> PhotoCaptions { get; set; }

        [Anonymizable(null, typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_posts", "posts", new string[0])]
        public List<string> Posts { get; set; }

        [Anonymizable("Status Updates:", typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_status", "statuses", new string[0])]
        public List<string> StatusUpdates { get; set; }

        [Anonymizable("Tagged Places:", typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_tagged_places", "tagged_places", new string[0])]
        public List<string> TaggedPlaces { get; set; }

        [Anonymizable("Titles of Posted Videos:", typeof(StringMD5Anonymizer), true)]
        [FacebookPermission("user_videos", "videos", new string[0])]
        public List<string> VideoTitles { get; set; }

        public override string DisplayDetail
        {
            get
            {
                return "(Facebook Data)";
            }
        }

        public FacebookDatum(DateTimeOffset timestamp)
            : base(timestamp)
        {
        }
    }
}