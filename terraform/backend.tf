terraform {
  backend "s3" {
    bucket       = "terraform-s3-bucket-3as4d45"
    key          = "terraform.tfstate"
    region       = "eu-central-1"
    use_lockfile = true
    encrypt      = true
  }
}
