﻿using System;
using Espera.Core.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Espera.Core.Tests
{
    [TestClass]
    public class LibraryTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateAdmin_PasswordIsNull_ThrowsArgumentNullException()
        {
            var library = new Library.Library();

            try
            {
                library.CreateAdmin(null);
            }

            finally
            {
                library.Dispose();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateAdmin_PasswordIsEmpty_ThrowsArgumentException()
        {
            var library = new Library.Library();

            try
            {
                library.CreateAdmin(String.Empty);
            }

            finally
            {
                library.Dispose();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateAdmin_PasswordIsWhitespace_ThrowsArgumentException()
        {
            var library = new Library.Library();

            try
            {
                library.CreateAdmin(" ");
            }

            finally
            {
                library.Dispose();
            }
        }

        [TestMethod]
        public void CreateAdmin_PasswordIsTestPassword_AdministratorIsCreated()
        {
            var library = new Library.Library();

            library.CreateAdmin("TestPassword");

            Assert.IsTrue(library.IsAdministratorCreated);

            library.Dispose();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ChangeToAdmin_PasswordIsNull_ThrowsArgumentNullException()
        {
            var library = new Library.Library();

            try
            {
                library.ChangeToAdmin(null);
            }

            finally
            {
                library.Dispose();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPasswordException))]
        public void ChangeToAdmin_PasswordIsNotCorrent_ThrowsInvalidOperationException()
        {
            var library = new Library.Library();
            library.CreateAdmin("TestPassword");

            try
            {
                library.ChangeToAdmin("WrongPassword");
            }

            finally
            {
                library.Dispose();
            }
        }

        [TestMethod]
        public void ChangeToAdmin_PasswordIsCorrent_AccessModeIsAdministrator()
        {
            var library = new Library.Library();
            library.CreateAdmin("TestPassword");
            library.ChangeToAdmin("TestPassword");

            Assert.AreEqual(AccessMode.Administrator, library.AccessMode);

            library.Dispose();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlaySong_UserIsNotAdministrator_ThrowsInvalidOperationException()
        {
            var library = new Library.Library();
            library.CreateAdmin("TestPassword");
            library.ChangeToUser();

            try
            {
                library.PlaySong(0);
            }

            finally
            {
                library.Dispose();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PlaySong_IndexIsLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            var library = new Library.Library();

            try
            {
                library.PlaySong(-1);
            }

            finally
            {
                library.Dispose();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlayNextSong_UserIsNotAdministrator_ThrowsInvalidOperationException()
        {
            var library = new Library.Library();
            library.CreateAdmin("TestPassword");
            library.ChangeToUser();

            try
            {
                library.PlayNextSong();
            }

            finally
            {
                library.Dispose();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlayPreviousSong_PlaylistIsEmpty_ThrowsInvalidOperationException()
        {
            var library = new Library.Library();

            try
            {
                library.PlayPreviousSong();
            }

            finally
            {
                library.Dispose();
            }
        }
    }
}