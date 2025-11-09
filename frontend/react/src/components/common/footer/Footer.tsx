import privacyNoticyPdf from "../../../assets/resources/Privacy Notice.pdf";
import privacyPolicyPdf from "../../../assets/resources/Privacy Policy.pdf";
import Link from "../link/Link";
import "./Footer.scss";

const Footer = () => {
  return (
    <footer className="footer">
      <p className="footer__text">© 2025 EPAM Systems. All rights reserved.</p>

      <ul className="footer__links">
        <li>
          <Link
            size="small"
            color="white"
            href={privacyPolicyPdf}
            target="_blank"
            rel="noopener noreferrer"
          >
            Privacy Policy
          </Link>
        </li>

        <li>
          <Link
            size="small"
            color="white"
            href={privacyNoticyPdf}
            target="_blank"
            rel="noopener noreferrer"
          >
            Privacy Notice
          </Link>
        </li>
      </ul>
    </footer>
  );
};

export default Footer;
