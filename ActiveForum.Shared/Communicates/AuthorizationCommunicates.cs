namespace ActiveForum.Shared.Communicates
{
    public static class AuthorizationCommunicates
    {
        public static string PageWithoutNonAuthorizedContent
        { 
            get { return "Wystąpił problem z autoryzacją. Spróbuj zalogować się ponownie lub kliknij link 'Kontakt' znajdujący się w dolnej części strony w celu uzyskania pomocy."; } 
        }
    }
}
